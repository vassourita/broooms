namespace Broooms.Catalog.Core.Services;

using AutoMapper;
using Broooms.Catalog.Core.Data;
using Broooms.Catalog.Core.Dtos;
using Broooms.Catalog.Core.Entities;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(
        IProductRepository productRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        this._productRepository = productRepository;
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Product>> FindByFiltersAsync(ProductSearchDto filters)
    {
        try
        {
            var entities = await this._productRepository.FindByFiltersAsync(filters);
            await this._unitOfWork.CommitAsync();
            return entities;
        }
        catch (Exception)
        {
            await this._unitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task<Product> FindByIdAsync(Guid id)
    {
        try
        {
            var entity = await this._productRepository.FindByIdAsync(id);
            await this._unitOfWork.CommitAsync();
            return entity;
        }
        catch (Exception)
        {
            await this._unitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task<Product> CreateAsync(ProductDto dto)
    {
        try
        {
            var entity = await this._productRepository.AddAsync(this._mapper.Map<Product>(dto));
            await this._unitOfWork.CommitAsync();
            return entity;
        }
        catch (Exception)
        {
            await this._unitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task<Product> UpdateAsync(ProductDto dto)
    {
        try
        {
            var entity = await this._productRepository.UpdateAsync(this._mapper.Map<Product>(dto));
            await this._unitOfWork.CommitAsync();
            return entity;
        }
        catch (Exception)
        {
            await this._unitOfWork.RollbackAsync();
            return null;
        }
    }

    public async Task RemoveAsync(Guid id)
    {
        try
        {
            await this._productRepository.RemoveAsync(id);
            await this._unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await this._unitOfWork.RollbackAsync();
        }
    }
}
