using Dboard.Components.Pages;
using Docker.DotNet.Models;
using Docker.DotNet;

namespace Dboard.Services
{
    public class DockerService
    {
        public DockerClient dockerClient;

        public DockerService()
        {

            // 创建 Docker 客户端
            dockerClient = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();


        }

        public Task<IList<ContainerListResponse>> GetContainerListAsync()
        {
            // 使用 Docker 客户端进行操作
            var containers = dockerClient.Containers.ListContainersAsync(new ContainersListParameters() { All = true });

            return containers;
        }

        public Task Restart(string id)
        {
            return dockerClient.Containers.RestartContainerAsync(id, new ContainerRestartParameters());
        }
    }
}
