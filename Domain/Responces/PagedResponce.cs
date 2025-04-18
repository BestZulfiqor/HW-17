using System.Net;

namespace Domain.Responces;

public class PagedResponce<T> : Response<T>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    public PagedResponce(T data, int pageNumber, int pageSize, int totalRecords) : base(data)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }
    public PagedResponce(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {

    }
}
