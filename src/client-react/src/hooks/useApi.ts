import axios, { AxiosError, AxiosResponse } from 'axios';
import { useCallback, useEffect, useState } from 'react';

const apiClient = axios.create({
  baseURL: 'http://localhost:5000/api',
});

apiClient.interceptors.request.use(undefined, (error) => Promise.reject(error));

const responseBody = <T>(response: AxiosResponse<T>) => response?.data;

interface ApiState<T> {
  data: T | null;
  error: AxiosError | null;
  loading: boolean;
}

export const useQuery = <T>(url: string, manual = false) => {
  const [state, setState] = useState<ApiState<T>>({
    data: null,
    error: null,
    loading: true,
  });

  const fetchData = useCallback(async () => {
    setState((prev) => ({ ...prev, loading: true }));
    try {
      const data = await responseBody(await apiClient.get<T>(url));
      setState({ data, error: null, loading: false });
    } catch (error) {
      setState({ data: null, error: error as AxiosError, loading: false });
    }
  }, [url]);

  useEffect(() => {
    if (!manual) {
      fetchData();
    }
  }, [url, manual, fetchData]);

  return { ...state, fetch: fetchData };
};

export const useMutation = <T>(url: string, method: 'post' | 'put' | 'patch' | 'del') => {
  const [state, setState] = useState<ApiState<T>>({
    data: null,
    error: null,
    loading: false,
  });

  const mutate = async (body?: object) => {
    setState({ data: null, error: null, loading: true });
    try {
      let data: T | null = null;
      if (method === 'del') {
        data = await responseBody(await apiClient.delete<T>(url));
      } else {
        data = await responseBody(await apiClient[method]<T>(url, body));
      }
      setState({ data, error: null, loading: false });
    } catch (error) {
      setState({ data: null, error: error as AxiosError, loading: false });
    }
  };

  return { ...state, mutate };
};
