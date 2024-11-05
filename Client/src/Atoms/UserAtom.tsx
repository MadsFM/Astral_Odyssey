import {atom} from "jotai/vanilla/atom";
import {UserDto} from "../Models/Api.ts";


export const userAtom = atom<UserDto[]>([]);