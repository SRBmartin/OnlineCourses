using System.ServiceModel;
using OnlineCourses.Contracts;

namespace OnlineCourses.DataProcessing.Services;

public class WcfInformationSystemService : IInformationSystemService
{
    private readonly ChannelFactory<IInformationSystemService> _factory;

    public WcfInformationSystemService(string serviceUrl)
    {
        var binding = new BasicHttpBinding
        {
            OpenTimeout    = TimeSpan.FromSeconds(4),
            SendTimeout    = TimeSpan.FromSeconds(4),
            ReceiveTimeout = TimeSpan.FromSeconds(30)
        };
        _factory = new ChannelFactory<IInformationSystemService>(binding, new EndpointAddress(serviceUrl));
    }

    private TResult Call<TResult>(Func<IInformationSystemService, TResult> action)
    {
        var channel = _factory.CreateChannel();
        try
        {
            var result = action(channel);
            ((IClientChannel)channel).Close();
            return result;
        }
        catch
        {
            ((IClientChannel)channel).Abort();
            throw;
        }
    }

    public List<Course> GetAllCourses() =>
        Call(c => c.GetAllCourses());

    public List<ParticipantActivity> GetActivities(Guid courseId, DateTime from, DateTime to) =>
        Call(c => c.GetActivities(courseId, from, to));
}
