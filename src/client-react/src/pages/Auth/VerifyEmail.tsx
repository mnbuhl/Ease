import { Head, Link, useForm } from '@inertiajs/react';
import { FormEventHandler } from 'react';
import Button from '../../components/Button';
import GuestLayout from '../../layouts/GuestLayout';

type Props = {
  status?: string;
  canVerifyEmail: boolean;
};

const VerifyEmail = ({ status, canVerifyEmail }: Props) => {
  const { post, processing } = useForm({});

  const submit: FormEventHandler = (e) => {
    e.preventDefault();

    post('/verify-email/resend');
  };

  return (
    <>
      <Head title="Email Verification" />

      <div className="mb-4 text-sm text-gray-600">
        Thanks for signing up! Before getting started, could you verify your email address by
        clicking on the link we just emailed to you? If you didn't receive the email, we will gladly
        send you another.
      </div>

      {status === 'verification-link-sent' && (
        <div className="mb-4 font-medium text-sm text-green-600">
          A new verification link has been sent to the email address you provided during
          registration.
        </div>
      )}

      <form onSubmit={submit}>
        <div className="mt-4 flex items-center justify-between">
          <Button loading={processing || !canVerifyEmail}>Resend Verification Email</Button>

          <Link
            href={'/auth/logout'}
            method="post"
            as="button"
            className="underline text-sm text-gray-600 hover:text-gray-900 rounded-md focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
          >
            Log out
          </Link>
        </div>
      </form>
    </>
  );
};

VerifyEmail.layout = (page: React.ReactNode) => <GuestLayout>{page}</GuestLayout>;

export default VerifyEmail;
