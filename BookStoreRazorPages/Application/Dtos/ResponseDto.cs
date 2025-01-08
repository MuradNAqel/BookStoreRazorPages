namespace BookStoreRazorPages.Application.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        public void SetFailureWithError(string errKey, string errMessage)
        {
            Success = false;
            Errors.Add(errKey, errMessage);
        }

        public void SetSuccessWithPayload(T data)
        {
            Success = true;
            Data = data;
        }
    }
}
