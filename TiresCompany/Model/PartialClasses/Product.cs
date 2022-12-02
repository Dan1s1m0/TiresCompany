using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiresCompany.Model
{
    public partial class Product
    {
        public string ImagePath { 
            get {
                if (Image == null)
                {
                    return "\\Assets\\Images\\picture.png";
                }
                else
                {
                    return "/Assets/Images" + Image;
                }
            }
        }
       //Список материалов
        public string MaterialList
        {
            get
            {
                string materials = "Материалы: ";
                List<string> arrayMaterials=new List<string> { };
                List<ProductMaterial> arrayActiveProduct = ProductMaterial.Where(x=>x.ProductID == ID).ToList();
                foreach (var item in arrayActiveProduct)
                {
                    arrayMaterials.Add(item.Material.Title.ToString());
                }
                if (arrayMaterials.Count==0)
                {
                    materials = "";
                }
                else
                {
                    materials += String.Join(",", arrayMaterials);
                }
                return materials;
            }
        }
        //стоимость 
        public string CostProduct
        {
            get
            {
                double costProduct = 0;
                List<ProductMaterial> arrayActiveProduct = ProductMaterial.Where(x=>x.ProductID == ID).ToList();
                foreach (var item in arrayActiveProduct)
                {
                    costProduct += Convert.ToDouble(item.Count) * Convert.ToDouble(item.Material.Cost);
                }
                return costProduct.ToString();
            }
        }


    }
}
