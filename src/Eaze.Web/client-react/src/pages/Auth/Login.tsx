import { Head, Link, useForm } from '@inertiajs/react';
import GuestLayout from '../../layouts/GuestLayout';
import Label from '../../components/forms/Label';
import Input from '../../components/forms/Input';
import Error from '../../components/forms/Error';
import { useEffect } from 'react';
import Button from '../../components/forms/Button';
import Checkbox from '../../components/forms/Checkbox';

interface Props {
  status: string;
  canResetPassword: boolean;
}

const Login = ({ status, canResetPassword }: Props) => {
  const { data, setData, post, processing, errors, reset } = useForm({
    email: '',
    password: '',
    remember: false,
  });

  useEffect(() => {
    return () => reset('password');
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    post('/auth/login');
  };

  return (
    <>
      <Head title="Log in" />

      {status && <div className="mb-4 font-medium text-sm text-green-600">{status}</div>}

      <form onSubmit={handleSubmit}>
        <div>
          <Label htmlFor="email" value="Email" />

          <Input
            id="email"
            type="email"
            name="email"
            value={data.email}
            className="mt-1 block w-full"
            autoComplete="username"
            isFocused={true}
            onChange={(e) => setData('email', e.target.value)}
            required
          />

          <Error message={errors.email} className="mt-2" />
        </div>

        <div className="mt-4">
          <Label htmlFor="password" value="Password" />

          <Input
            id="password"
            type="password"
            name="password"
            value={data.password}
            className="mt-1 block w-full"
            autoComplete="current-password"
            onChange={(e) => setData('password', e.target.value)}
            required
          />

          <Error message={errors.password} className="mt-2" />
        </div>

        <div className="block mt-4">
          <label className="flex items-center">
            <Checkbox
              name="remember"
              checked={data.remember}
              onChange={(e) => setData('remember', e.target.checked)}
            />
            <span className="ml-2 text-sm text-gray-600 dark:text-gray-400">Remember me</span>
          </label>
        </div>

        <div className="flex items-center justify-end mt-4">
          {canResetPassword && (
            <Link
              href={'/auth/forgot-password'}
              className="underline text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-100 rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 dark:focus:ring-offset-gray-800"
            >
              Forgot your password?
            </Link>
          )}

          <Button variant="secondary" type="submit" className="ml-4" loading={processing}>
            Log in
          </Button>
        </div>
      </form>
    </>
  );
};

Login.layout = (page: JSX.Element) => <GuestLayout>{page}</GuestLayout>;

export default Login;
