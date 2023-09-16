import { Head, Link, useForm } from '@inertiajs/react';
import GuestLayout from '../../layouts/GuestLayout';
import { FormEventHandler, useEffect } from 'react';
import Label from '../../components/forms/Label';
import Input from '../../components/forms/Input';
import Error from '../../components/forms/Error';
import Button from '../../components/forms/Button';

const Register = () => {
  const { data, setData, post, processing, errors, reset } = useForm({
    name: '',
    email: '',
    password: '',
    passwordConfirmation: '',
  });

  useEffect(() => {
    return () => reset('password', 'passwordConfirmation');

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const submit: FormEventHandler = (e) => {
    e.preventDefault();

    post('/auth/register');
  };

  return (
    <>
      <Head title="Register" />

      <form onSubmit={submit}>
        <div>
          <Label htmlFor="name" value="Name" />

          <Input
            id="name"
            name="name"
            value={data.name}
            className="mt-1 block w-full"
            autoComplete="name"
            isFocused={true}
            onChange={(e) => setData('name', e.target.value)}
            required
          />

          <Error message={errors.name} className="mt-2" />
        </div>

        <div className="mt-4">
          <Label htmlFor="email" value="Email" />

          <Input
            id="email"
            type="email"
            name="email"
            value={data.email}
            className="mt-1 block w-full"
            autoComplete="username"
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
            autoComplete="new-password"
            onChange={(e) => setData('password', e.target.value)}
            required
          />

          <Error message={errors.password} className="mt-2" />
        </div>

        <div className="mt-4">
          <Label htmlFor="passwordConfirmation" value="Confirm Password" />

          <Input
            id="passwordConfirmation"
            type="password"
            name="passwordConfirmation"
            value={data.passwordConfirmation}
            className="mt-1 block w-full"
            autoComplete="new-password"
            onChange={(e) => setData('passwordConfirmation', e.target.value)}
            required
          />

          <Error message={errors.passwordConfirmation} className="mt-2" />
        </div>

        <div className="flex items-center justify-end mt-4">
          <Link
            href={'/auth/login'}
            className="underline text-sm text-gray-600 dark:text-gray-400 hover:text-gray-900 dark:hover:text-gray-100 rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 dark:focus:ring-offset-gray-800"
          >
            Already registered?
          </Link>

          <Button variant="primary" className="ml-4" disabled={processing}>
            Register
          </Button>
        </div>
      </form>
    </>
  );
};

Register.layout = (page: JSX.Element) => <GuestLayout>{page}</GuestLayout>;

export default Register;
