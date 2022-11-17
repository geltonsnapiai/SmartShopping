using System;

namespace SmartShopping.Dtos
{
    public struct ProductDto
    {
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public float Price { get; set; }
        public string Shop { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public void GenerateTagsFromName()
        {
            // TODO: Improve
            string[] tagNames = Name.Split(' ');
            Tags = Tags.Concat(tagNames).ToArray();
        }
    }
 }