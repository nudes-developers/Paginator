# Paginator

Paginator is a c# lib for handling pagination and transfer of data and use on client libraries

## Installation

if you need all packages (mostly on Backend with ASPNETCore)
```
dotnet add package Nudes.Paginator
```

but you can also use its individual packages and configure to your specific need
```
dotnet add package Nudes.Paginator.Core
dotnet add package Nudes.Paginator.Validator
```


## Usage

### Nudes.Paginator.Core
The core lib contains only what you would call a DTO or the protocol/contract of pagination, it is used to make an contract between server and clients on how paged data will be transfered.
If you are on a client you will require only this or one of it's specific libs that we are planning like blazor and xamarin

Usually requests will received on the following query data: `Page`; `PageSize`; `Field`; `Sorting Direction` and any custom data that you will use to filter data

> NOTICE: For user-friendlyness this lib considers the first page as 1 and not 0

Return data will always follow the following contract
```json
{
    "pagination" : {
        "page": 13,
        "pageSize": 40,
        "sorting": [
            {
                "field": "property",
                "sortDirection": 0
            }
        ],
        "total": 5000,
        "pageCount": 125,
        "isFirstPage": false,
        "isLastPage": false
    },
    "items" : []
}
```

The *in facto* pagination methods are extensions on top of `IQueryable<T>` hence can work on top of EFCore

```csharp
PageRequest request = GetPageRequest();
IQueryable<T> data = GetData();

var total = await data.CountAsync(); //required to calculate page data correctly

//the second parameter of PaginateBy is the default field for Sorting, if request has an field specified the specified field will be used if Descending Sort is desired use PagineByDescending
var items = await data.PaginateBy(request, d=> d.Field) 
    .Select(d => MapToResult(d))
    .ToListAsync();

return request.ToResult(items, total);
```
### FluentValidation
This lib will be mostly used on server as well and is a default implementation of an validator of PagedRequest, you can use it directly or you can create your own and include it's validation
```csharp
public class SpecificValidator : AbstractValidator<MyRequest>
{
    public SpecificValidator()
    {
        //MyRequest must inherit from PagedRequest
        this.Include(new PageRequestValidator<MyRequest>()); 
        
        //insert your specific business validation here like
        RuleFor(d=> d.Type).NotEmpty();
    }
}
```

### Client side and sorting

On the client side sorting information will be received through the query param `sorting`, this param is mapped to an array and should be passed as such: `?sorting[0].field=fieldName&sorting[0].sortDirection=ascending`
being an array also means we can have a multi-field sorting.

This lib also accepts complex property as a field name, just call separated through a '.' as you would on c# code `firstLevelProperty.secondLevelProperty.thirdLevelProperty`, just remember that these properties will be matched against the type you are invoking `.PaginateBy` so the clients would have access to your DTOs and should not know the real types you are storing on your database, so be sure to map/project to DTO before paginating

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.