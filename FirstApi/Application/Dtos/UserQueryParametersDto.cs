namespace FirstApi.Application.Dtos
{
    public class UserQueryParametersDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? Nome { get; set; }
        public string? Email { get; set; }
    }
}