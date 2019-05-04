using System;

namespace StoreSim.Library
{
    public class Product
    {
        private string _name;
        private float _price;
        private int _quantity;

        public float Price { get => _price; }

        public string Name { get => _name; }

        public int Quantity 
        { 
            get => _quantity; 
            
            set 
            {
                if(value < 0)
                {
                    throw new ArgumentException("Cannot fulfill order");
                }
            }
        }

        public Product(string name, float price, int quantity)
        {
            if(name == string.Empty)
            {
                throw new ArgumentException("Product must have name");
            }
            _name = name;

            if(price <= 0)
            {
                throw new ArgumentException("Product must have price");
            }
            _price = price;

            if(quantity < 0)
            {
                throw new ArgumentException("Product cannot have negative stock");
            }
            _quantity = quantity;
        }
    }
}
