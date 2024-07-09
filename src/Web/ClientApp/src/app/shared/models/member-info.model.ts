export interface MemberInfo {
  name: string;
  position: string;
  image: string;
  networks: NetworkInfo[];
};

export interface NetworkInfo {
  name: string;
  url: string;
};
