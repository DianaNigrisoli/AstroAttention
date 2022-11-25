namespace Assets.ScriptsDirectionGame
{
    public class MyVector3
    {
        private float x;
        private float y;
        private float z;

        public MyVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float X
        {
            get => x;
            set => x = value;
        }

        public float Y
        {
            get => y;
            set => y = value;
        }

        public float Z
        {
            get => z;
            set => z = value;
        }
    }
}