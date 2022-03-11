namespace Broooms.Catalog.Core.Services;

using AutoMapper;
using Broooms.Catalog.Core.Data;
using Broooms.Catalog.Core.Dtos;
using Broooms.Catalog.Core.Entities;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<Product>> FindByFiltersAsync(ProductSearchDto filters)
    {
        try
        {
            var entities = await this._productRepository.FindByFiltersAsync(filters);
            await this._productRepository.UnitOfWork.CommitAsync();
            return entities;
        }
        catch (Exception)
        {
            await this._productRepository.UnitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        try
        {
            var entity = await this._productRepository.FindByIdAsync(id);
            await this._productRepository.UnitOfWork.CommitAsync();
            return entity;
        }
        catch (Exception)
        {
            await this._productRepository.UnitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task<Product> CreateAsync(ProductCreateDto dto)
    {
        try
        {
            var entity = await this._productRepository.AddAsync(this._mapper.Map<Product>(dto));
            await this._productRepository.UnitOfWork.CommitAsync();
            return entity;
        }
        catch (Exception)
        {
            await this._productRepository.UnitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task<Product> UpdateAsync(ProductUpdateDto dto)
    {
        try
        {
            var actual = await this._productRepository.FindByIdAsync(dto.Id);
            if (actual == null)
            {
                return null;
            }
            actual.Update(
                name: dto.Name,
                description: dto.Description,
                priceInCents: dto.PriceInCents
            );
            var updated = await this._productRepository.UpdateAsync(actual);
            await this._productRepository.UnitOfWork.CommitAsync();
            return updated;
        }
        catch (Exception)
        {
            await this._productRepository.UnitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        try
        {
            await this._productRepository.RemoveAsync(id);
            await this._productRepository.UnitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await this._productRepository.UnitOfWork.RollbackAsync();
        }
    }
}
