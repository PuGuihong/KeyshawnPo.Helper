using KeyshawnPo.DAO;
using KeyshawnPo.IRepository;
using MysqlConnectionString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Repository
{
    public class SysMenuRepository : RepositoryBase<sys_menu, string>, ISysMenuRepository
    {
    }
}
