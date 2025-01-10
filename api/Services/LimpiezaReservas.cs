using api.Data;

public class LimpiezaReservas : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer? _timer;

    public LimpiezaReservas(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Programa la tarea para que se ejecute cada día
        _timer = new Timer(EliminarReservasPasadas, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        return Task.CompletedTask;
    }

    private void EliminarReservasPasadas(object? state)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

        var hoy = DateTime.Now;

        var reservasAreasPasadas = dbContext.ReservaAreas.Where(r => r.End < hoy);
        var reservasImplementosPasadas = dbContext.ReservasImplementos.Where(r => r.End < hoy);

        dbContext.ReservaAreas.RemoveRange(reservasAreasPasadas);
        dbContext.ReservasImplementos.RemoveRange(reservasImplementosPasadas);

        dbContext.SaveChanges();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
