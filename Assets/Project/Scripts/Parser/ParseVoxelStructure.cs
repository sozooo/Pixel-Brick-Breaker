#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using Project.Scripts.FigureSystem;
using Project.Scripts.FigureSystem.Handling;

namespace Project.Scripts.Parser
{
	public class ParseVoxelStructure : MonoBehaviour
	{
		struct RawVoxelData
		{
			public Vector2 PositionF { get; }
			public string Color { get; }

			public RawVoxelData(Vector2 pos, string color)
			{
				PositionF = pos;
				Color = color;
			}
		}

		[MenuItem("Tools/Parse Voxel Structure into FigureConfig")]
		public static void Do()
		{
			if (Selection.assetGUIDs == null || Selection.assetGUIDs.Length == 0)
				throw new InvalidOperationException("At least 1 text file should be selected!");

			foreach (var guid in Selection.assetGUIDs)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				
				if (string.IsNullOrEmpty(path))
					continue;

				using var reader = new StreamReader(path);
				var raw = ParseRawVoxelsFromFile(reader);
				var shifted = ShiftToPositive(raw);
				SaveAsFigureConfig(shifted, Path.GetFileNameWithoutExtension(path));
			}
		}

		private static List<RawVoxelData> ParseRawVoxelsFromFile(StreamReader file)
		{
			if (file == null) 
				throw new ArgumentNullException(nameof(file));

			var list = new List<RawVoxelData>();
			string line;
			
			while ((line = file.ReadLine()) != null)
			{
				if (string.IsNullOrWhiteSpace(line)) 
					continue;
				
				line = line.Trim();
				
				if (line.Length == 0) 
					continue;
				
				if (line[0] == '#') 
					continue;

				var parts = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
				
				if (parts.Length < 4) 
					continue;

				if (!float.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float xf)) 
					continue;
				
				if (!float.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float yf)) 
					continue;
				
				string color = parts[3];
				list.Add(new RawVoxelData(new Vector2(xf, yf), color));
			}

			return list;
		}

		private static List<RawVoxelData> ShiftToPositive(List<RawVoxelData> raw)
		{
			if (raw == null || raw.Count == 0) 
				return new List<RawVoxelData>();

			float minX = raw.Min(r => r.PositionF.x);
			float minY = raw.Min(r => r.PositionF.y);

			return raw.Select(r => new RawVoxelData(new Vector2(r.PositionF.x - minX, r.PositionF.y - minY), r.Color)).ToList();
		}

		private static void SaveAsFigureConfig(List<RawVoxelData> voxels, string name)
		{
			if (string.IsNullOrEmpty(name)) name = "Figure";
			string assetDir = "Assets/Project/Prefabs/VoxelStructures/Difficulties";
			string assetPath = Path.Combine(assetDir, name + ".asset").Replace("\\", "/");

			var config = AssetDatabase.LoadAssetAtPath<FigureConfig>(assetPath);
			bool created = false;
			
			if (config == null)
			{
				config = ScriptableObject.CreateInstance<FigureConfig>();
				if (!Directory.Exists(assetDir)) Directory.CreateDirectory(assetDir);
				AssetDatabase.CreateAsset(config, assetPath);
				created = true;
			}

			var voxelList = new List<Voxel>();
			
			foreach (var v in voxels)
			{
				int rx = (int)Math.Round(v.PositionF.x, MidpointRounding.AwayFromZero);
				int ry = (int)Math.Round(v.PositionF.y, MidpointRounding.AwayFromZero);
				var pos = new Vector2Int(rx, ry);
				Color col = Color.white;
				if (!string.IsNullOrEmpty(v.Color))
				{
					ColorUtility.TryParseHtmlString("#" + v.Color, out col);
				}
				voxelList.Add(new Voxel(pos, col));
			}

			int minXi = voxelList.Count > 0 ? voxelList.Min(x => x.Position.x) : 0;
			int maxXi = voxelList.Count > 0 ? voxelList.Max(x => x.Position.x) : 0;
			int minYi = voxelList.Count > 0 ? voxelList.Min(x => x.Position.y) : 0;
			int maxYi = voxelList.Count > 0 ? voxelList.Max(x => x.Position.y) : 0;

			int width = Mathf.Abs(maxXi - minXi) + 1;
			int height = Mathf.Abs(maxYi - minYi) + 1;

			config.Initialize(voxelList, width, height, Vector3.one);

			EditorUtility.SetDirty(config);
			AssetDatabase.SaveAssets();
			
			if (created)
				AssetDatabase.Refresh();
		}
	}
}
#endif
