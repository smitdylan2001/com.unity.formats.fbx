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

public static class FbxConnection { 
  // virtual void Dispose()  { } 

  public enum EType {
    eNone = 0,
    eSystem = 1 << 0,
    eUser = 1 << 1,
    eSystemOrUser = eUser|eSystem,
    eReference = 1 << 2,
    eContains = 1 << 3,
    eData = 1 << 4,
    eLinkType = eReference|eContains|eData,
    eDefault = eUser|eReference,
    eUnidirectional = 1 << 7
  }

}

}
