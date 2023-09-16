import { Head, usePage } from '@inertiajs/react';
import { PageProps } from '../lib/utils/page-props';
import GuestLayout from '../layouts/GuestLayout';

const Index = () => {
  const { auth } = usePage<PageProps>().props;

  console.log(auth);

  return (
    <>
      <Head>
        <title>Index</title>
      </Head>
      <div>IndexPage</div>
    </>
  );
};

Index.layout = (page: JSX.Element) => <GuestLayout>{page}</GuestLayout>;

export default Index;
