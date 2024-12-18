/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface CreateUserDto {
  /**
   * @minLength 0
   * @maxLength 50
   */
  username: string;
  /**
   * @minLength 0
   * @maxLength 100
   */
  email: string;
  /**
   * @minLength 0
   * @maxLength 255
   */
  passwordhash: string;
  roleName?: string | null;
}

export interface LoginUserDto {
  username?: string | null;
  password?: string | null;
}

export interface Planet {
  /** @format int32 */
  planetid?: number;
  /**
   * @minLength 0
   * @maxLength 100
   */
  name?: string | null;
  description?: string | null;
  /** @format int32 */
  universeid?: number;
  isdiscovered?: boolean | null;
  quests?: Quest[] | null;
  universe?: Universe;
}

export interface Quest {
  /** @format int32 */
  questid?: number;
  /**
   * @minLength 0
   * @maxLength 100
   */
  title?: string | null;
  description?: string | null;
  /** @format int32 */
  planetid?: number | null;
  /** @format int32 */
  universeid?: number | null;
  iscompleted?: boolean | null;
  planet?: Planet;
  quizzes?: Quiz[] | null;
  universe?: Universe;
  userquestprogresses?: Userquestprogress[] | null;
}

export interface Quiz {
  /** @format int32 */
  quizid?: number;
  question?: string | null;
  /**
   * @minLength 0
   * @maxLength 100
   */
  answer?: string | null;
  hint?: string | null;
  /** @format int32 */
  questid?: number | null;
  quest?: Quest;
}

export interface Role {
  /** @format int32 */
  roleid?: number;
  /**
   * @minLength 0
   * @maxLength 50
   */
  rolename?: string | null;
  userroles?: Userrole[] | null;
}

export interface RoleCountDto {
  roleName?: string | null;
  /** @format int32 */
  userCount?: number;
}

export interface Scoreboard {
  /** @format int32 */
  scoreid?: number;
  /** @format int32 */
  userid?: number;
  /** @format int32 */
  points?: number;
  /** @format date-time */
  updatedat?: string | null;
  user?: User;
}

export interface Universe {
  /** @format int32 */
  universeid?: number;
  /**
   * @minLength 0
   * @maxLength 100
   */
  name?: string | null;
  description?: string | null;
  planets?: Planet[] | null;
  quests?: Quest[] | null;
}

export interface UpdateUserDto {
  /**
   * @minLength 0
   * @maxLength 50
   */
  username?: string | null;
  /**
   * @minLength 0
   * @maxLength 100
   */
  email?: string | null;
  /**
   * @minLength 0
   * @maxLength 255
   */
  newPassword?: string | null;
}

export interface User {
  /** @format int32 */
  userid?: number;
  /**
   * @minLength 0
   * @maxLength 50
   */
  username?: string | null;
  /**
   * @minLength 0
   * @maxLength 100
   */
  email?: string | null;
  /**
   * @minLength 0
   * @maxLength 255
   */
  passwordhash?: string | null;
  /** @format date-time */
  createdat?: string | null;
  scoreboards?: Scoreboard[] | null;
  userquestprogresses?: Userquestprogress[] | null;
  userroles?: Userrole[] | null;
}

export interface UserDto {
  /** @format int32 */
  userid?: number;
  username?: string | null;
  email?: string | null;
  passwordHash?: string | null;
  token?: string | null;
  /** @format date-time */
  createdat?: string | null;
  scoreboardIds?: number[] | null;
  userquestprogressIds?: number[] | null;
  roles?: string[] | null;
}

export interface Userquestprogress {
  /** @format int32 */
  progressid?: number;
  /** @format int32 */
  userid?: number;
  /** @format int32 */
  questid?: number;
  iscompleted?: boolean | null;
  /** @format date-time */
  lastupdated?: string | null;
  quest?: Quest;
  user?: User;
}

export interface Userrole {
  /** @format int32 */
  userid?: number;
  /** @format int32 */
  roleid?: number;
  /** @format date-time */
  createdat?: string | null;
  role?: Role;
  user?: User;
}

import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, HeadersDefaults, ResponseType } from "axios";
import axios from "axios";

export type QueryParamsType = Record<string | number, any>;

export interface FullRequestParams extends Omit<AxiosRequestConfig, "data" | "params" | "url" | "responseType"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseType;
  /** request body */
  body?: unknown;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> extends Omit<AxiosRequestConfig, "data" | "cancelToken"> {
  securityWorker?: (
    securityData: SecurityDataType | null,
  ) => Promise<AxiosRequestConfig | void> | AxiosRequestConfig | void;
  secure?: boolean;
  format?: ResponseType;
}

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public instance: AxiosInstance;
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private secure?: boolean;
  private format?: ResponseType;

  constructor({ securityWorker, secure, format, ...axiosConfig }: ApiConfig<SecurityDataType> = {}) {
    this.instance = axios.create({ ...axiosConfig, baseURL: axiosConfig.baseURL || "" });
    this.secure = secure;
    this.format = format;
    this.securityWorker = securityWorker;
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected mergeRequestParams(params1: AxiosRequestConfig, params2?: AxiosRequestConfig): AxiosRequestConfig {
    const method = params1.method || (params2 && params2.method);

    return {
      ...this.instance.defaults,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...((method && this.instance.defaults.headers[method.toLowerCase() as keyof HeadersDefaults]) || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected stringifyFormItem(formItem: unknown) {
    if (typeof formItem === "object" && formItem !== null) {
      return JSON.stringify(formItem);
    } else {
      return `${formItem}`;
    }
  }

  protected createFormData(input: Record<string, unknown>): FormData {
    if (input instanceof FormData) {
      return input;
    }
    return Object.keys(input || {}).reduce((formData, key) => {
      const property = input[key];
      const propertyContent: any[] = property instanceof Array ? property : [property];

      for (const formItem of propertyContent) {
        const isFileType = formItem instanceof Blob || formItem instanceof File;
        formData.append(key, isFileType ? formItem : this.stringifyFormItem(formItem));
      }

      return formData;
    }, new FormData());
  }

  public request = async <T = any, _E = any>({
    secure,
    path,
    type,
    query,
    format,
    body,
    ...params
  }: FullRequestParams): Promise<AxiosResponse<T>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const responseFormat = format || this.format || undefined;

    if (type === ContentType.FormData && body && body !== null && typeof body === "object") {
      body = this.createFormData(body as Record<string, unknown>);
    }

    if (type === ContentType.Text && body && body !== null && typeof body !== "string") {
      body = JSON.stringify(body);
    }

    return this.instance.request({
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type ? { "Content-Type": type } : {}),
      },
      params: query,
      responseType: responseFormat,
      data: body,
      url: path,
    });
  };
}

/**
 * @title API
 * @version 1.0
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  role = {
    /**
     * No description
     *
     * @tags Role
     * @name RoleCountList
     * @request GET:/Role/role-count
     */
    roleCountList: (params: RequestParams = {}) =>
      this.request<RoleCountDto[], any>({
        path: `/Role/role-count`,
        method: "GET",
        format: "json",
        ...params,
      }),
  };
  user = {
    /**
     * No description
     *
     * @tags User
     * @name RegisterCreate
     * @request POST:/User/register
     */
    registerCreate: (data: CreateUserDto, params: RequestParams = {}) =>
      this.request<UserDto, any>({
        path: `/User/register`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name GetAllList
     * @request GET:/User/getAll
     */
    getAllList: (params: RequestParams = {}) =>
      this.request<UserDto[], any>({
        path: `/User/getAll`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name GetByIdDetail
     * @request GET:/User/getById/{id}
     */
    getByIdDetail: (id: number, params: RequestParams = {}) =>
      this.request<UserDto, any>({
        path: `/User/getById/${id}`,
        method: "GET",
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name LoginCreate
     * @request POST:/User/login
     */
    loginCreate: (data: LoginUserDto, params: RequestParams = {}) =>
      this.request<UserDto, any>({
        path: `/User/login`,
        method: "POST",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name UserPartialUpdate
     * @request PATCH:/User/{id}
     */
    userPartialUpdate: (id: number, data: UpdateUserDto, params: RequestParams = {}) =>
      this.request<UserDto, any>({
        path: `/User/${id}`,
        method: "PATCH",
        body: data,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags User
     * @name UserDelete
     * @request DELETE:/User/{id}
     */
    userDelete: (id: number, params: RequestParams = {}) =>
      this.request<User, any>({
        path: `/User/${id}`,
        method: "DELETE",
        format: "json",
        ...params,
      }),
  };
}
