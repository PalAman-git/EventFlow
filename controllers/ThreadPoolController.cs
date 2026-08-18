using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ThreadPoolController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        ThreadPool.GetAvailableThreads(out int availableWorkerThreads,out int availableIoThreads) ;
        ThreadPool.GetMaxThreads(out int maxWorkerThreads,out int maxIoThreads);

        return Ok ("Hello");
    }
}