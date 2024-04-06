using System;

namespace LightStore.Application.Filtering.Product
{
    public class ProductFilterArgs
    {
        public ProductFilterArgs() { }

        public ProductFilterArgs(string search, Guid? categoryId)
        {
            Search = search;
            CategoryId = categoryId;
        }

        public string Search { get; init; }
        public Guid? CategoryId { get; init; }
    }
}
