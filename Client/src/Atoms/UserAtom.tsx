import {atom} from "jotai";
import {UserDto} from "../Models";


export const userAtom = atom<UserDto[]>([]);