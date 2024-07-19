import sys, os, subprocess
from os import path
from subprocess import PIPE

ROOT_DIR = path.dirname(path.abspath(__file__))
MAX_RUNTIME = 5
BUILD_DIRNAME="out"
LOG_DIRNAME = "log"
LOG_CONFIG_FILE = "config"
LOG_TEMP_FILE = "temp"
LOG_FILE = "log"

def print_instruction() -> None:
    """Print instruction for users.
    """
    print("Please enter a target program:\n \
        - Put a semicolon (;) between each statement.\n \
        - Do not put a semicolon (;) at the end of the program.\n \
        - Enter \"x\" to exit the program.")
    print("============================================================")
    
def build() -> bool:
    """Build the type inference program.

    Returns:
        bool: build result.
    """
    try:
        orig_cwd = os.getcwd()
        os.chdir(path.join(ROOT_DIR))
        run_cmd("dotnet build -o %s" % BUILD_DIRNAME)
        os.chdir(orig_cwd)
        return True
    except Exception as e:
        return False

def read_config(config: str) -> int:
    """Read the ID of the current type inference from the config file and return the id.

    Args:
        config (str): config file path.

    Returns:
        int: ID of the current type inference.
    """
    with open(config) as f:
        log_id = int(f.read())
    return log_id

def clear_file(file: str):
    """Clear the file.

    Args:
        file (str): file to clear.
    """
    with open(file, "w") as f:
        f.write("")
        
def run_cmd(cmd_str: str, check=True, timeout: int=None) -> str:
    """_summary_

    Args:
        cmd_str (str): command to run.
        check (bool, optional): check option. Defaults to True.
        timeout (int, optional): timeout option. Defaults to None.

    Returns:
        str: result of the command execution.
    """
    args = cmd_str.split()
    p = subprocess.run(args, check=check, stdout=PIPE, stderr=PIPE,
                       timeout=timeout)
    return "".join(map(chr, p.stdout))

def infer(binary: str, program: str) -> str:
    """Execute type inference

    Args:
        binary (str): path of the type inference binary program.
        program (str): path of the target program.

    Returns:
        str: result of the type inference.
    """
    cmd = "%s %s" % (binary, program)
    try:
        output = run_cmd(cmd, timeout=MAX_RUNTIME)
        return output
    except subprocess.TimeoutExpired:
        print("Timeout")
        return None
    except subprocess.CalledProcessError as e:
        print("Error")
        return None

def save_output(log_file: str, program: str, output: str) -> None:
    """Save the result of the type inference.

    Args:
        log_file (str): path of the log file to save.
        program (str): path of the target program.
        output (str): type inference result.
    """
    with open(program) as f:
        prog = f.read()
    with open(log_file, "a") as f:
        print("result:", output.strip(), file=f)
        print("\nprogram:\n" + prog, file=f)

def is_end_of_program(line):
    if line == "" or line == "\n" or line[-1] == ';' or line[-1] == '{' or line[-1] == '}':
        return False
    else:
        return True
    

def main() -> None:
    """Main function.
    """
    log_config = path.join(ROOT_DIR, LOG_DIRNAME, LOG_CONFIG_FILE)
    log_temp = path.join(ROOT_DIR, LOG_DIRNAME, LOG_TEMP_FILE)
    
    log_id = read_config(log_config)
    while(1):
        print_instruction()
        clear_file(log_temp)
        log_file = path.join(ROOT_DIR, LOG_DIRNAME, LOG_FILE + str(log_id))
        with open(log_temp, "a") as f:
            line = input()
            while line !=  "x":
                f.write(line + "\n")
                if is_end_of_program(line):
                    break
                line = input()
            
        if line != "x":
            binary = path.join(ROOT_DIR, BUILD_DIRNAME, "CMinusPtr")
            output = infer(binary, log_temp)
            if output is not None:
                print("------------------------------------------------------------")
                print("result:", output.strip())
                print("============================================================")
                save_output(log_file, log_temp, output)
                log_id += 1
        else:
            with open(log_config, 'w') as f:
                f.write(str(log_id))
            break
            
if __name__ == "__main__":
    print("============================================================")
    print("CMinusPtr Interpreter")
    print("============================================================")
    print("Building the program. Please wait...")
    build_success = build()
    print("Done!")
    print("============================================================")
    if build_success:
        main()
    else:
        print("Build Failed!")