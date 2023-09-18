import { Head, useForm } from '@inertiajs/react';
import { FormEventHandler, useEffect } from 'react';
import Label from '../../components/forms/Label';
import Input from '../../components/forms/Input';
import Error from '../../components/forms/Error';
import Button from '../../components/Button';
import GuestLayout from '../../layouts/GuestLayout';

const ResetPassword = ({ token, email }: { token: string; email: string }) => {
  const { data, setData, post, processing, errors, reset } = useForm({
    token: token,
    email: email,
    password: '',
    passwordConfirmation: '',
  });

  useEffect(() => {
    return () => reset('password', 'passwordConfirmation');
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const submit: FormEventHandler = (e) => {
    e.preventDefault();

    post('/password/reset');
  };

  return (
    <>
      <Head title="Reset Password" />

      <form onSubmit={submit}>
        <div>
          <Label htmlFor="email" value="Email" />

          <Input
            id="email"
            type="email"
            name="email"
            value={data.email}
            className="mt-1 block w-full"
            autoComplete="username"
            onChange={(e) => setData('email', e.target.value)}
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
            isFocused={true}
            onChange={(e) => setData('password', e.target.value)}
          />

          <Error message={errors.password} className="mt-2" />
        </div>

        <div className="mt-4">
          <Label htmlFor="passwordConfirmation" value="Confirm Password" />

          <Input
            type="password"
            name="passwordConfirmation"
            value={data.passwordConfirmation}
            className="mt-1 block w-full"
            autoComplete="new-password"
            onChange={(e) => setData('passwordConfirmation', e.target.value)}
          />

          <Error message={errors.passwordConfirmation} className="mt-2" />
        </div>

        <div className="flex items-center justify-end mt-4">
          <Button variant="primary" className="ml-4" disabled={processing}>
            Reset Password
          </Button>
        </div>
      </form>
    </>
  );
};

ResetPassword.layout = (page: JSX.Element) => <GuestLayout>{page}</GuestLayout>;

export default ResetPassword;
