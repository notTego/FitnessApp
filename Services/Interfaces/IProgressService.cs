using FitnessTracker.DTOs;
using FitnessTracker.Entities;

public interface IProgressService
{
	Task<List<Progress>> GetAllAsync(int userId);
	Task<Progress> AddAsync(ProgressDTO dto, int userId);
}