namespace Core
{
    public class ParseUtilities
    {
        public long ToGroupId(long id)
        {
            if (id < 0)
            {
                return id;
            }

            return id * -1;
        }

        public long ToPositiveId(long id)
        {
            if (id > 0)
            {
                return id;
            }

            return id * -1;
        }
    }
}