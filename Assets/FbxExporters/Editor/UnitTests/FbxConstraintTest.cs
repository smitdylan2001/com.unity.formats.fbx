﻿using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.Animations;

namespace FbxExporters.UnitTests
{
    public class FbxConstraintTest : ExporterTestBase
    {
        /// <summary>
        /// Create and begin setting up constraint of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toExport"></param>
        /// <returns></returns>
        protected T CreateConstraint<T>(out List<Object> toExport) where T : Component, IConstraint
        {
            // setup constrained object and sources
            var constrainedGO = new GameObject("constrained");
            var sourceGO = new GameObject("source");

            toExport = new List<Object>();
            toExport.Add(constrainedGO);
            toExport.Add(sourceGO);

            sourceGO.transform.localPosition = new Vector3(1, 2, 3);
            sourceGO.transform.localRotation = Quaternion.Euler(20, 45, 90);
            sourceGO.transform.localScale = new Vector3(1, 1.5f, 2);

            var uniConstraint = SetupConstraintWithSources<T>(constrainedGO, new List<GameObject>() { sourceGO });

            uniConstraint.constraintActive = true;
            uniConstraint.locked = false;
            uniConstraint.weight = 0.75f;

            return uniConstraint;
        }

        /// <summary>
        /// Export constraint of type T and check some of its properties on import.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniConstraint"></param>
        /// <param name="toExport"></param>
        /// <returns>The imported constraint</returns>
        protected T ExportAndCheckConstraint<T>(T uniConstraint, Object[] toExport) where T : Component, IConstraint
        {
            // export and compare
            var exportedGO = ExportConstraints(toExport);

            // get exported constraint
            var expConstraint = exportedGO.GetComponentInChildren<T>();
            Assert.That(expConstraint, Is.Not.Null);

            TestSourcesMatch(uniConstraint, expConstraint);

            Assert.That(expConstraint.constraintActive, Is.EqualTo(uniConstraint.constraintActive));
            Assert.That(expConstraint.locked, Is.EqualTo(true)); // locked always imports as true
            Assert.That(expConstraint.weight, Is.EqualTo(uniConstraint.weight).Within(0.0001f));

            return expConstraint;
        }
        
        [Test]
        public void TestPositionConstraintExport()
        {
            List<Object> toExport;
            var posConstraint = CreateConstraint<PositionConstraint>(out toExport);

            // setup position specific properties
            posConstraint.translationAtRest = new Vector3(0.5f, 0.5f, 0.5f);
            posConstraint.translationAxis = Axis.X | Axis.Z;
            posConstraint.translationOffset = new Vector3(0.25f, 0.5f, 0.75f);

            var expConstraint = ExportAndCheckConstraint(posConstraint, toExport.ToArray());

            Assert.That(expConstraint.translationAtRest, Is.EqualTo(posConstraint.translationAtRest));
            Assert.That(expConstraint.translationAxis, Is.EqualTo(posConstraint.translationAxis));
            Assert.That(expConstraint.translationOffset, Is.EqualTo(posConstraint.translationOffset));
        }

        [Test]
        public void TestRotationConstraint()
        {
            List<Object> toExport;
            var rotConstraint = CreateConstraint<RotationConstraint>(out toExport);

            // setup rotation specific properties
            rotConstraint.rotationAtRest = new Vector3(30, 20, 10);
            rotConstraint.rotationOffset = new Vector3(45, 60, 180);
            rotConstraint.rotationAxis = Axis.Y;

            var expConstraint = ExportAndCheckConstraint(rotConstraint, toExport.ToArray());

            Assert.That(AreEqual(expConstraint.rotationAtRest, rotConstraint.rotationAtRest, 0.001), Is.True);
            Assert.That(expConstraint.rotationAxis, Is.EqualTo(rotConstraint.rotationAxis));
            Assert.That(expConstraint.rotationOffset, Is.EqualTo(rotConstraint.rotationOffset));
        }

        [Test]
        public void TestScaleConstraint()
        {
            // setup constrained object and sources
            List<Object> toExport;
            var scaleConstraint = CreateConstraint<ScaleConstraint>(out toExport);

            scaleConstraint.scaleAtRest = new Vector3(2, 2, 2);
            scaleConstraint.scaleOffset = new Vector3(1, 0.3f, 0.7f);
            scaleConstraint.scalingAxis = Axis.X | Axis.Y | Axis.Z;

            // export and compare
            var expConstraint = ExportAndCheckConstraint(scaleConstraint, toExport.ToArray());

            Assert.That(expConstraint.scalingAxis, Is.EqualTo(scaleConstraint.scalingAxis));
            Assert.That(expConstraint.scaleAtRest, Is.EqualTo(scaleConstraint.scaleAtRest));
            Assert.That(expConstraint.scaleOffset, Is.EqualTo(scaleConstraint.scaleOffset));
        }

        [Test]
        public void TestParentConstraintExport()
        {

        }

        [Test]
        public void TestAimConstraintExport()
        {

        }


        public bool AreEqual(Vector3 a, Vector3 b, double epsilon = 0.0001)
        {
            return Vector3.SqrMagnitude(a - b) < epsilon;
        }

        /// <summary>
        /// Setup the constraint component on the constrained object with the given sources.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="constrained"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        protected T SetupConstraintWithSources<T>(GameObject constrained, List<GameObject> sources) where T : Component, IConstraint
        {
            var constraint = constrained.AddComponent<T>();
            Assert.That(constraint, Is.Not.Null);

            int sourceCount = sources.Count;
            for(int i = 0;  i < sourceCount; i++)
            {
                var source = sources[i];
                var cSource = new ConstraintSource();
                cSource.sourceTransform = source.transform;
                cSource.weight = i / ((float)sourceCount);

                int index = constraint.AddSource(cSource);
                Assert.That(index, Is.EqualTo(i));
            }
            Assert.That(constraint.sourceCount, Is.EqualTo(sourceCount));
            return constraint;
        }

        protected void TestSourcesMatch(IConstraint original, IConstraint exported)
        {
            var origSources = new List<ConstraintSource>();
            original.GetSources(origSources);

            var expSources = new List<ConstraintSource>();
            exported.GetSources(expSources);

            Assert.That(expSources.Count, Is.EqualTo(origSources.Count));

            for(int i = 0; i < origSources.Count; i++)
            {
                var origSource = origSources[i];
                var expSource = expSources[i];

                Assert.That(expSource.sourceTransform, Is.EqualTo(origSource.sourceTransform));
                Assert.That(expSource.weight, Is.EqualTo(origSource.weight));
            }
        }

        protected GameObject ExportConstraints(Object[] toExport)
        {
            var filename = GetRandomFileNamePath();
            var exportedGO = ExportSelection(filename, toExport);
            ImportConstraints(filename);
            return exportedGO;
        }

        protected void ImportConstraints(string filename)
        {
            ModelImporter modelImporter = AssetImporter.GetAtPath(filename) as ModelImporter;
            modelImporter.importConstraints = true;
            AssetDatabase.ImportAsset(filename);
        }
    }
}