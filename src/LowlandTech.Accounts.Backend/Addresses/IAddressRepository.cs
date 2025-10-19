namespace LowlandTech.Accounts.Backend.Addresses;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Address>> ListAsync(CancellationToken ct = default);
    Task<Address> AddAsync(Address entity, CancellationToken ct = default);
    Task UpdateAsync(Address entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
