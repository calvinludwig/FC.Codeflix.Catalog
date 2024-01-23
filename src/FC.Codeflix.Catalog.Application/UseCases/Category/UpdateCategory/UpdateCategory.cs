using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IUpdateCategory
{
    public async Task<CategoryOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.Get(request.Id, cancellationToken);
        category.Update(request.Name, request.Description);
        if (request.IsActive != category.IsActive)
            if (request.IsActive) category.Activate();
            else category.Deactivate();
        await categoryRepository.Update(category, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        
        return CategoryOutput.FromCategory(category);
    }
}