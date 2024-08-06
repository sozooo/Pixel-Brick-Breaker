#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ParseVoxelStructure : MonoBehaviour
{
	private const string VoxelLayer = "Voxel";

	struct RawVoxelData
	{
		private readonly Vector3 _position;
		private readonly string _color;

		public RawVoxelData(Vector3 position, string color)
		{
			_position = position;
			_color = color;
		}

		public Vector3 Position => _position;
		public string Color => _color;
	}

	[MenuItem("Tools/Parse Voxel Structure into Prefab")]
	public static void Do()
	{
		if (Selection.assetGUIDs.Length == 0)
			throw new InvalidOperationException("At least 1 text file should be selected!");

		foreach(string guid in Selection.assetGUIDs)
        {
			var path = AssetDatabase.GUIDToAssetPath(guid);

			StreamReader file = new(path);
			var rawVoxelDatas = ParseRawVoxelsFromFile(file);
			file.Close();

			var rootGameObject = CreateVoxelStructure(rawVoxelDatas);
			Selection.activeObject = rootGameObject;
			rootGameObject.layer = LayerMask.NameToLayer(VoxelLayer);

			PrefabUtility.SaveAsPrefabAsset(rootGameObject,
				$"Assets/Prefabs/VoxelStructures/Difficulties/{Path.GetFileName(path)}.prefab");
			DestroyImmediate(rootGameObject);
        }
	}

	private static List<RawVoxelData> ParseRawVoxelsFromFile(StreamReader file)
	{
		if (file == null)
			throw new InvalidOperationException(nameof(file));

		var rawVoxelDatas = new List<RawVoxelData>();
		string line;

		while ((line = file.ReadLine()) != null)
		{
			if (line[0] == '#' || line[0] == ' ')
			{
				continue;
			}
			else
			{
				var split = line.Split();

                Vector3 position = new(float.Parse(split[0]),
					float.Parse(split[2]), float.Parse(split[1]));

                rawVoxelDatas.Add(new RawVoxelData(position, split[3]));
			}
		}

		return rawVoxelDatas;
	}

	private static GameObject CreateVoxelStructure(List<RawVoxelData> rawVoxelData)
	{
		if (rawVoxelData == null)
			throw new InvalidOperationException(nameof(rawVoxelData));

		GameObject root = new();

		GameObject primitive = AssetDatabase.LoadAssetAtPath(
			$"Assets/Prefabs/Primitive.prefab",
			typeof(GameObject)) as GameObject;

		if (primitive == null)
			throw new InvalidCastException(nameof(primitive));

		foreach (RawVoxelData voxelData in rawVoxelData)
        {
			GameObject cube = Instantiate(primitive);

			cube.transform.SetParent(root.transform);
			cube.transform.position = voxelData.Position;
			cube.layer = LayerMask.NameToLayer(VoxelLayer);

			TrailRenderer trail = GetTrailRenderer(cube);
			trail.enabled = false;

			Material material = GetOrCreateVoxelMaterial(voxelData.Color);
			cube.GetComponent<Renderer>().sharedMaterial = material;
		}

		return root;
	}

	private static Material GetOrCreateVoxelMaterial(string colorCode)
	{
		Material material = AssetDatabase.LoadAssetAtPath(
			$"Assets/Prefabs/VoxelStructures/Materials/{colorCode}.mat",
			typeof(Material)) as Material;

		if (material == null)
			material = CreateVoxelMaterial(colorCode);

		return material;
	}

	private static Material CreateVoxelMaterial(string colorCode)
	{
		var material = new Material(Shader.Find("FlexibleCelShader/Cel Silhouette"));
        ColorUtility.TryParseHtmlString($"#{colorCode}", out Color color);

        if (color == null)
			throw new InvalidOperationException($"Couldn't parse color: #{colorCode}");

		material.color = color;
		AssetDatabase.CreateAsset(material, $"Assets/Prefabs/VoxelStructures/Materials/{colorCode}.mat");

		return material;
	}

	private static TrailRenderer GetTrailRenderer(GameObject voxel)
    {
		if (voxel == null)
			throw new InvalidOperationException(nameof(voxel));

        voxel.TryGetComponent(out TrailRenderer trail);

        if (trail == null)
			throw new InvalidOperationException("Trail not found");

		return trail;
    }
}
#endif
