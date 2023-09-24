using ShopApp.Enums;
using ShopApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopApp.Entites
{
    public partial class Item
    {
        public static string GenSizeTemplate(List<string> sizes)
        {
            List<SizeModel> sizesModel = new List<SizeModel>();
            foreach (string clothSize in sizes)
            {
                sizesModel.Add(new SizeModel() { size = clothSize});
            }

            string sizesJson = JsonSerializer.Serialize(sizesModel);

            return sizesJson;
        }
    }
}
