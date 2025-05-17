using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhisperBranch.Application.Dispatcher.CommandHandling
{
    public interface ICommand<out TResult> { }
}
