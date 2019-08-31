namespace System
{
    public static class MyExtensions
    {
        public static string ToStringContent(this char[] array)
        {   
            string content = "";
            
            foreach(var c in array)
            {
                content += c;
            }

            return content;
        }
    }
}