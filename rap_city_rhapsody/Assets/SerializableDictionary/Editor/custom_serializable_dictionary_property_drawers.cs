using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// ==================== Other ======================
[CustomPropertyDrawer(typeof(String_StoryElement_Dict))]
public class Custom_AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer { }

//[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class Custom_AnySerializableDictionaryStoragePropertyDrawer : SerializableDictionaryStoragePropertyDrawer { }