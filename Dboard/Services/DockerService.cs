using Dboard.Components.Pages;
using Docker.DotNet.Models;
using Docker.DotNet;
using AntDesign;
using log4net;
using Dboard.Handlers;

namespace Dboard.Services
{
    public class DockerService
    {

        protected ILog log = LogManager.GetLogger("service");

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


        public async void ReDeployByName(string containerName)
        {

            //获取容器信息
            var oldContainer = (await dockerClient.Containers
                .ListContainersAsync(new ContainersListParameters() { All = true }))
                .FirstOrDefault(f => f.Names.FirstOrDefault().Substring(1) == containerName);

            if (oldContainer == null)
            {
                throw new Exception("container not found");
            }

            ReDeploy(oldContainer.ID);
        }

        public async Task<string> ReDeploy(string ID)
        {

            var oldContainerInfo = await dockerClient.Containers.InspectContainerAsync(ID);
            if (oldContainerInfo == null)
            {
                log.Error("container not found.");
                return null;
            }

            //停止旧窗口
            await dockerClient.Containers.StopContainerAsync(ID, new ContainerStopParameters() { });

            //删除旧容器
            await dockerClient.Containers.RemoveContainerAsync(ID, new ContainerRemoveParameters() { Force = true });

            //删除旧image
            await dockerClient.Images.DeleteImageAsync(oldContainerInfo.Config.Image, new ImageDeleteParameters() { Force = true });

            //拉取新image
            await pullImage(oldContainerInfo.Config.Image);

            //发布新容器 
            CreateContainerResponse newContainer = await dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters(oldContainerInfo.Config));

            //启动
            await dockerClient.Containers.StartContainerAsync(newContainer.ID, new ContainerStartParameters());

            return newContainer.ID;
        }

        public Task pullImage(string imageName, string tag = "latest")
        {
            try
            {
                return dockerClient.Images.CreateImageAsync(new ImagesCreateParameters() { FromImage = imageName, Tag = tag }, new AuthConfig() { }, new ProgressReporter());
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return Task.FromException(ex);
            }

        }


        public Task Delete(string ID)
        {
            try
            {
                return dockerClient.Images.DeleteImageAsync(ID, new ImageDeleteParameters() { Force = true });
            }
            catch (Exception ex)
            {
                log.Error(ex);

                return Task.FromException(ex);
            }

        }
    }
}
