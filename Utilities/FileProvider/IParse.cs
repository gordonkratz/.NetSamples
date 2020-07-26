namespace StockOptionApp.FIleDownload
{
    public interface IParse<T>
    {
        bool TryParse(string line, out T data);
    }
}
