using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private Dictionary<string, IService> _services = new Dictionary<string, IService>();

    public static ServiceLocator Current { get; private set; }

    private ServiceLocator() { }

    public static void Initialize() 
    {
        Current = new ServiceLocator();
    }

    public void Register<T>(T service) where T : IService
    {
        var key = GetServiceName<T>();
        if (_services.ContainsKey(key))
        {
            Debug.LogError(
                $"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
            throw new InvalidOperationException();
        }

        _services.Add(key, service);
    }

    public T Get<T>() where T : IService 
    {
        var key = GetServiceName<T>();
        if (_services.TryGetValue(key, out var service) == false) 
        {
            Debug.LogError($"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }

        return (T)service;
    }

    public void UnRegister<T>() where T : IService
    {
        var key = GetServiceName<T>();
        if (_services.ContainsKey(key) == false)
        {
            Debug.LogError(
                $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
            
            return;
        }

        _services.Remove(key);
    }

    private string GetServiceName<T>() where T : IService 
    {
        return typeof(T).Name;
    }
}

public interface IService 
{

}
