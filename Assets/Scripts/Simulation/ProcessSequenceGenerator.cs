using System.Collections.Generic;
using UnityEngine;

public class ProcessSequenceGenerator
{
    public static (Dictionary<int, List<ProcessData>>, int) GenerateSequence(int processCount)
    {
        SortedList<int, List<ProcessData>> sequence = new SortedList<int, List<ProcessData>>();

        List<ProcessData> list = new List<ProcessData>();

        int totalDuration = 0;
        for (int i = 0; i < processCount; i++)
        {
            ProcessData processData = new ProcessData
            {
                Duration = Random.Range(SimulationManager.Instance.simulationSettings.minProcessDuration, SimulationManager.Instance.simulationSettings.maxProcessDuration),
                StartTime = totalDuration == 0 ? 0 : Random.Range(0, totalDuration + 1),
            };
            totalDuration += (int)processData.Duration;
            list.Add(processData);
        }

        foreach (ProcessData processData in list)
        {
            if (!sequence.ContainsKey(processData.StartTime))
                sequence.Add(processData.StartTime, new List<ProcessData>());
            sequence[processData.StartTime].Add(processData);
        }

        totalDuration = 0;
        int processId = 1;
        for (int i = 0; i < sequence.Count; i++)
        {
            (int time, List<ProcessData> processes) = (sequence.Keys[i], sequence.Values[i]);

            int fragmentSum = 0;
            for (int j = 0; j < processes.Count; j++)
            {
                processes[j] = new ProcessData
                {
                    Name = $"Process {processId}",
                    Duration = processes[j].Duration,
                    StartTime = processes[j].StartTime,
                    Id = processId,
                };
                fragmentSum += (int)processes[j].Duration;
                processId++;
            }

            totalDuration += fragmentSum;
            if (fragmentSum < time - totalDuration)
            {
                totalDuration += time - fragmentSum;
            }
        }

        return (new Dictionary<int, List<ProcessData>>(sequence), totalDuration);
    }
}
