namespace Polyclinic.Contracts;

public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    TDto Create(TCreateUpdateDto dto);           
    TDto? Get(TKey dtoId);                      
    List<TDto> GetAll();                        
    TDto Update(TCreateUpdateDto dto, TKey dtoId); 
    bool Delete(TKey dtoId);                   
}