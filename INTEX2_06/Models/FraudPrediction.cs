using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

public class FraudPrediction
{
    private readonly InferenceSession session;

    public FraudPrediction(string modelPath)
    {
        session = new InferenceSession(modelPath);
    }

    public float[] PredictFraud(float[][] inputData)
    {
        int batchSize = inputData.Length;
        int featureCount = inputData[0].Length;
        var tensor = new DenseTensor<float>(new Memory<float>(inputData.SelectMany(a => a).ToArray()), new[] { batchSize, featureCount });
        var input = NamedOnnxValue.CreateFromTensor<float>("float_input", tensor);

        var outputs = session.Run(new List<NamedOnnxValue> { input });
        var predictions = outputs.FirstOrDefault()?.AsTensor<float>().ToArray();
        return predictions;
    }

    public void Dispose()
    {
        session.Dispose();
    }
}
