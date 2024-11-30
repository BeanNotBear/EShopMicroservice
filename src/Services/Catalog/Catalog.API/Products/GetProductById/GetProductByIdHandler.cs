
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById
{
	public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
	public record GetProductByIdResult(Product Product);

	internal class GetProductByIdHandler(IDocumentSession session)
		: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{
		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			var product = await session.LoadAsync<Product>(query.Id);
			if (product == null)
			{
				throw new NotFoundException($"Can not found with id: {query.Id}");
			}
			var result = new GetProductByIdResult(product);
			return result;
		}
	}
}
