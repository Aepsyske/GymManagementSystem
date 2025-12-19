using GymManagementBL.Service.ViewModels.AnalyticsViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBL.Service.Interfaces
{
    public interface IAnalyticsService
    {
        AnalyticsViewModel GetAllAnalytics();
    }
}
