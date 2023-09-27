using Bogus;
using Microsoft.AspNetCore.Mvc;
using Nudes.Paginator.Core;

namespace Sample;

[Route("/")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public ActionResult<PageResult<Person>> List([FromQuery] PageRequest pageRequest, [FromServices] List<Person> dataSource)
    {
        var query = dataSource.AsQueryable();

        var total = query.Count();

        var items = query.PaginateBy(pageRequest, d => d.Id);

        return Ok(pageRequest.ToResult(items, total));
    }
}
