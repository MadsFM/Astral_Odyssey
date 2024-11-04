import {atom} from "jotai/vanilla/atom";
import {UserDto} from "../Api.ts";


export const userAtom = atom<UserDto[]>([]);