import styles from './MoreAboutButton.module.scss';

type MoreAboutButtonProps = {
  fromValue: string;
  toValue: string;
  arrowUp?: boolean;
  testId?: string;
  onClick?: () => void;
};

export const MoreAboutButton = ({ fromValue, toValue, arrowUp = true, testId, onClick }: MoreAboutButtonProps) => {
  return (
    <button className={styles.button} data-testid={testId} onClick={onClick} type="button">
      {fromValue}/{toValue}: about {arrowUp ? '↑' : '↓'}
    </button>
  );
};
