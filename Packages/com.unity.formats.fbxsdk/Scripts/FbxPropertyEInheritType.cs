//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------

namespace UnityEngine.Formats.FbxSdk {

public class FbxPropertyEInheritType : FbxProperty {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;

  internal FbxPropertyEInheritType(global::System.IntPtr cPtr, bool cMemoryOwn) : base(GlobalsPINVOKE.FbxPropertyEInheritType_SWIGUpcast(cPtr), cMemoryOwn) {
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(FbxPropertyEInheritType obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public override void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
      base.Dispose();
    }
  }

  public FbxPropertyEInheritType Set(FbxTransform.EInheritType pValue) {
    FbxPropertyEInheritType ret = new FbxPropertyEInheritType(GlobalsPINVOKE.FbxPropertyEInheritType_Set(swigCPtr, (int)pValue), false);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxTransform.EInheritType Get() {
    FbxTransform.EInheritType ret = (FbxTransform.EInheritType)GlobalsPINVOKE.FbxPropertyEInheritType_Get(swigCPtr);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxTransform.EInheritType EvaluateValue(FbxTime pTime, bool pForceEval) {
    FbxTransform.EInheritType ret = (FbxTransform.EInheritType)GlobalsPINVOKE.FbxPropertyEInheritType_EvaluateValue__SWIG_0(swigCPtr, FbxTime.getCPtr(pTime), pForceEval);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxTransform.EInheritType EvaluateValue(FbxTime pTime) {
    FbxTransform.EInheritType ret = (FbxTransform.EInheritType)GlobalsPINVOKE.FbxPropertyEInheritType_EvaluateValue__SWIG_1(swigCPtr, FbxTime.getCPtr(pTime));
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public FbxTransform.EInheritType EvaluateValue() {
    FbxTransform.EInheritType ret = (FbxTransform.EInheritType)GlobalsPINVOKE.FbxPropertyEInheritType_EvaluateValue__SWIG_2(swigCPtr);
    if (GlobalsPINVOKE.SWIGPendingException.Pending) throw GlobalsPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

}

}
