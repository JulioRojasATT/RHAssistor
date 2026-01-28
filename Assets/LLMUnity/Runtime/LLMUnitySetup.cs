/// @file
/// @brief File implementing helper functions for setup and process management.
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.IO.Compression;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

/// @defgroup llm LLM
/// @defgroup template Chat Templates
/// @defgroup utils Utils
namespace LLMUnity
{
    public delegate void EmptyCallback();
}
