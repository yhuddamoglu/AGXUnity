﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using AGXUnity;
using GUI = AGXUnityEditor.Utils.GUI;

namespace AGXUnityEditor.Tools
{
  [CustomTool( typeof( AGXUnity.Collide.Mesh ) )]
  public class ShapeMeshTool : ShapeTool
  {
    public AGXUnity.Collide.Mesh Mesh { get { return Shape as AGXUnity.Collide.Mesh; } }

    public ShapeMeshTool( AGXUnity.Collide.Shape shape )
      : base( shape )
    {
    }

    public override void OnAdd()
    {
      base.OnAdd();
    }

    public override void OnRemove()
    {
      base.OnRemove();
    }

    public override void OnPreTargetMembersGUI( InspectorEditor editor )
    {
      base.OnPreTargetMembersGUI( editor );

      var sourceObjects = Mesh.SourceObjects;
      var singleSource  = sourceObjects.FirstOrDefault();

      if ( editor.IsMultiSelect ) {
        var undoCollection = new List<Object>();
        foreach ( var target in editor.targets )
          undoCollection.AddRange( ( target as AGXUnity.Collide.Mesh ).GetUndoCollection() );
        Undo.RecordObjects( undoCollection.ToArray(), "Mesh source" );
      }
      else
        Undo.RecordObjects( Mesh.GetUndoCollection(), "Mesh source" );

      var newSingleSource = GUI.ShapeMeshSourceGUI( singleSource, InspectorEditor.Skin );
      if ( newSingleSource != null ) {
        if ( editor.IsMultiSelect ) {
          foreach ( var target in editor.targets )
            ( target as AGXUnity.Collide.Mesh ).SetSourceObject( newSingleSource );
        }
        else
          Mesh.SetSourceObject( newSingleSource );
      }

      GUI.Separator();
    }
  }
}
