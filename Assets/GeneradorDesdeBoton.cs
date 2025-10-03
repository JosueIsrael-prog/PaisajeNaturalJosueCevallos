using UnityEngine;

public class GeneradorDesdeBoton : MonoBehaviour
{
    public void Generar()
    {
        // Crear Terreno
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 512;
        terrainData.size = new Vector3(200, 30, 200);

        GameObject terreno = Terrain.CreateTerrainGameObject(terrainData);
        terreno.name = "TerrenoBonito";

        // Agregar textura blanca
        TerrainLayer layer = new TerrainLayer();
        layer.diffuseTexture = Texture2D.whiteTexture;
        terrainData.terrainLayers = new TerrainLayer[] { layer };

        // Simular montañas
        float[,] alturas = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];
        for (int x = 0; x < alturas.GetLength(0); x++)
        {
            for (int y = 0; y < alturas.GetLength(1); y++)
            {
                alturas[x, y] = Mathf.PerlinNoise(x * 0.03f, y * 0.03f) * 0.1f;
            }
        }
        terrainData.SetHeights(0, 0, alturas);

        // Árboles (cilindros verdes)
        for (int i = 0; i < 20; i++)
        {
            float x = Random.Range(0f, 200f);
            float z = Random.Range(0f, 200f);
            float y = terrainData.GetInterpolatedHeight(x / 200f, z / 200f);

            GameObject arbol = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            arbol.transform.position = new Vector3(x, y, z);
            arbol.transform.localScale = new Vector3(1f, 4f, 1f);
            arbol.GetComponent<Renderer>().material.color = Color.green;
            arbol.name = "Arbol_" + i;
        }

        // Casas (cubos rojos)
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(10f, 190f);
            float z = Random.Range(10f, 190f);
            float y = terrainData.GetInterpolatedHeight(x / 200f, z / 200f);

            GameObject casa = GameObject.CreatePrimitive(PrimitiveType.Cube);
            casa.transform.position = new Vector3(x, y + 1, z);
            casa.transform.localScale = new Vector3(5, 3, 5);
            casa.GetComponent<Renderer>().material.color = Color.red;
            casa.name = "Casa_" + i;
        }

        Debug.Log("✅ ¡Escena generada desde botón con éxito!");
    }
}
