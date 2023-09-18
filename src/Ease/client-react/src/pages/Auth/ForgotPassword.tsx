import { Head, useForm } from '@inertiajs/react';
import { FormEventHandler } from 'react';
import Input from '../../components/forms/Input';
import Error from '../../components/forms/Error';
import Button from '../../components/Button';
import GuestLayout from '../../layouts/GuestLayout';

type Props = {
  status?: string;
  canResetPassword: boolean;
};

const ForgotPassword = ({ status, canResetPassword }: Props) => {
  const { data, setData, post, processing, errors } = useForm({
    email: '',
  });

  const submit: FormEventHandler = (e) => {
    e.preventDefault();

    post('/password/forgot');
  };

  return (
    <>
      <Head title="Forgot Password" />

      <div className="mb-4 text-sm text-gray-600">
        Forgot your password? No problem. Just let us know your email address and we will email you
        a password reset link that will allow you to choose a new one.
      </div>

      {status && <div className="mb-4 font-medium text-sm text-green-600">{status}</div>}

      <form onSubmit={submit}>
        <Input
          id="email"
          type="email"
          name="email"
          value={data.email}
          className="mt-1 block w-full"
          isFocused={true}
          onChange={(e) => setData('email', e.target.value)}
        />

        <Error message={errors.email} className="mt-2" />

        <div className="flex items-center justify-end mt-4">
          <Button
            variant="primary"
            className="ml-4"
            loading={processing}
            disabled={!canResetPassword}
          >
            Email Password Reset Link
          </Button>
        </div>
      </form>
    </>
  );
};

ForgotPassword.layout = (page: JSX.Element) => <GuestLayout>{page}</GuestLayout>;

export default ForgotPassword;
