namespace Editor
{
    internal interface IAccessor
    {
        List<PrimitiveTemplate> Primitives { get; }
    }

    public class PrimitiveCollection : IAccessor
    {
        private readonly List<PrimitiveTemplate> primitives = new List<PrimitiveTemplate>();
        List<PrimitiveTemplate> IAccessor.Primitives => primitives;
    }

    public static class PrimitiveCollectionMethods
    {
        public static void Add(this PrimitiveCollection collection, PrimitiveTemplate primitive)
        {
            if (primitive is not null)
            {
                var clone = primitive.Clone();
                if (clone is not null)
                {
                    ((IAccessor)collection).Primitives.Add(clone);
                }
            }
        }

        public static void RemoveLast(this PrimitiveCollection collection)
        {
            var primitives = ((IAccessor)collection).Primitives;
            if (primitives.Count > 0)
            {
                primitives.RemoveAt(primitives.Count - 1);
            }
        }

        public static void Draw(this PrimitiveCollection collection, Graphics graphics)
        {
            foreach (PrimitiveTemplate primitive in ((IAccessor)collection).Primitives)
            {
                primitive.Draw(graphics);
            }
        }

        public static PrimitiveTemplate GetLast(this PrimitiveCollection collection)
        {
            return ((IAccessor)collection).Primitives.Last();
        }

        public static void Clear(this PrimitiveCollection collection)
        {
            ((IAccessor)collection).Primitives.Clear();
        }
    }
}
