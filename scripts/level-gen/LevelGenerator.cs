using Godot;

namespace Game.LevelGeneration
{
    public enum LevelType
    {
        RegularDungeon,
        LavaDungeon,
        EtherealPlane,
    }

    public abstract partial class LevelGenerator : Node
    {
        private uint m_LevelWidth;
        private uint m_LevelHeight;
        private uint[,] m_LevelGrid;
        private LevelType m_LevelType;

        public LevelGenerator(uint levelWidth, uint levelHeight, LevelType levelType)
        {
            m_LevelWidth = levelWidth;
            m_LevelHeight = levelHeight;
            m_LevelGrid = new uint[m_LevelWidth, m_LevelHeight];
            m_LevelType = levelType;
        }

        public abstract void GenerateLevel();

        protected bool IsInBounds(uint x, uint y)
        {
            return x < m_LevelWidth && y < m_LevelHeight;
        }

        protected uint? GetCell(uint x, uint y)
        {
            if (!IsInBounds(x, y))
            {
                GD.PrintErr("Attempted to access cell outside of level bounds");
                return null;
            }
            return m_LevelGrid[x, y];
        }
    }
}
