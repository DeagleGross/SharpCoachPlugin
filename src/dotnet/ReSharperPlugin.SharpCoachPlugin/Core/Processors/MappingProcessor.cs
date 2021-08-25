using ReSharperPlugin.SharpCoachPlugin.Core.Providers;

namespace ReSharperPlugin.SharpCoachPlugin.Core.Processors
{
    public class MappingProcessor
    {
        private readonly ModelInfoProvider _model1;
        private readonly ModelInfoProvider _model2;

        public MappingProcessor(ModelInfoProvider model1, ModelInfoProvider model2)
        {
            _model1 = model1;
            _model2 = model2;
        }
    }
}