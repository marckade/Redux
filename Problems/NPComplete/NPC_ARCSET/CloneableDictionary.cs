// //CloneableDictionary.cs
// using System;
// using System.Collections.Generic;

// namespace API.Problems.NPComplete.NPC_ARCSET;

// class CloneableDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TValue : ICloneable
//     {
//     public CloneableDictionary<TKey, TValue> Clone()
//         {
//         CloneableDictionary<TKey, TValue> clone = new CloneableDictionary<TKey, TValue>();
//         foreach (KeyValuePair<TKey, TValue> kvp in this)
//             {
//             clone.Add(kvp.Key, (TValue) kvp.Value.Clone());
//             }
//         return clone;
//         }
//     }

//     //Credit: codeproject.com/Questions/165992/how-do-i-clone-a-dictionary-in-C