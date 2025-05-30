# Project_SUA
개발 기간 : 2024.09 ~ 2025.06

## 프로젝트 소개
<img src="https://github.com/user-attachments/assets/4e07ad18-fd83-42df-8ec9-b3dc63c2bfeb" width="60%" height="65%"/>

본 프로젝트는 노약자 및 장애인 재활을 위한 운전 시뮬레이션입니다.
- 구성품 : 32인치 모니터 3개, 스티어링 휠, 기어 노브, 페달, 햅틱 의자
- 가상 환경 : 오프로드 환경 
- 인지 테스트 종류 : 단기 기억력, 주의 집중력, 순차 기억력
  
사용자는 스티어링 휠, 기어 노브, 페달을 이용하여 차량을 제어할 수 있습니다.  
사용자는 주행 중에 인지 테스트를 진행하며, 이 인지 테스트는 MMSE을 참고하였습니다.  
오프로드 환경은 흙길, 자갈길, 진흙길로 구성되어 있으며, 사용자는 주행 중에 햅틱 의자를 통해 각기 다른 진동 피드백을 전달받습니다.  
주행 종료 후 인지 테스트에 대한 결과를 제공하여 사용자의 인지 기능에 대한 피드백을 줄 수 있도록 하였습니다.

# 팀원 구성 및 역할 분담
한승희 (팀장) / 이승빈 / 안예준 / 정희연

## 팀원 전체
- 목표 시스템 시나리오 상세 설계
- 유니티를 이용한 개발
- 버그 테스트 및 개선

#### 한승희 (팀장)
- 햅틱 의자 개발
- 시뮬레이션과 햅틱 의자 연동 시스템 개발
- 가상 환경 개발
- 가상 환경 렌더링

#### 이승빈
- 사용자 입력 기반 화면 전환 시스템 개발
- 차량 제어 개발
- 단기 기억력 테스트 기능 개발
- 주의 집중력 테스트 기능 개발

#### 안예준
- 가상 환경 설계 및 개발
- 가상 환경 렌더링
- 사운드 삽입

#### 정희연
- 사용자 UI/UX 설계 및 개발
- 순차 기억력 테스트 기능 개발
- 인지 기능 평가 결과 출력 시스템 개발
- 최종 발표 자료 제작

## 개발 환경
[시뮬레이션]  
Engine : Unity (버전: 6000.0.42)  
프로그래밍 언어 : C#  
  
[햅틱 의자]  
통합 개발 환경 : IAR Embedded Workbench  
프로그래밍 언어 : C  
구성품 : 진동 모터 18개(자화전자회사), 의자, ARM Cortex-M3 보드  
  
[협업 툴]    
버전 및 이슈 관리 : Github  
회의 기록 및 일정 관리 : Notion  

## 프로젝트 주요 기능

### 시스템 구성도
<img src="https://github.com/user-attachments/assets/8014871b-02c1-42fb-9f77-caacc5669fdc" width="60%" height="60%"/>

### 시나리오
<img src="https://github.com/user-attachments/assets/a1b56e7a-8ce0-4fd9-b539-ca8940d915f4" width="60%" height="60%"/>

### 가상환경
<img src="https://github.com/user-attachments/assets/aacc0495-7ff0-4abc-8b3e-030cbfb8438c" width="60%" height="60%"/>
<img src="https://github.com/user-attachments/assets/f57e2a4c-99ff-4af8-b0e4-a3ddf62824fa" width="60%" height="60%"/>

### 인지 기능 테스트
<img src="https://github.com/user-attachments/assets/2e7987e5-c9e9-4251-9b28-1a0a6e7e22ca" width="60%" height="60%"/>

#### 1. 단기 기억력 테스트
주행 중 잠시 나타나는 방향 표시 화살표를 기억한 뒤 바로 등장하는 갈림길에서 올바른 방향으로 주행해야 하는 과제를 통해 테스트를 진행합니다.  
<img src="https://github.com/user-attachments/assets/b22b429a-1271-4355-b90c-447e1e93a65f" width="60%" height="60%"/>

#### 2. 주의 집중력 테스트
도로 위로 갑작스럽게 떨어지는 바위 장애물을 회피하는 상황을 통해 테스트를 진행합니다.  
<img src="https://github.com/user-attachments/assets/0d8043b1-e706-43be-861b-94623c478287" width="50%" height="50%"/>
 
#### 3. 순차 기억력 테스트
주행 도중 순서대로 등장하는 동물을 기억한 뒤 주행 종료 후 동물의 등장 순서를 맞추는 문제를 통해 테스트를 진행합니다.  
<img src="https://github.com/user-attachments/assets/8b9ac553-acae-4215-add9-8bf7bc12d20c" width="60%" height="60%"/>

#### 4. 인지 기능 테스트 결과
단기 기억력, 주의 집중력, 순차 기억력 테스트에 대한 수행 결과를 시각적으로 표시합니다.  
<img src="https://github.com/user-attachments/assets/ec201a0b-8fc6-44bc-95a1-021f2cd866bf" width="60%" height="60%"/>

### 햅틱 의자
#### 1. 햅틱 의자 구성
사용자가 진동을 잘 느낄 수 있는 부위인 등판과 좌판에 진동 모터를 부착하였습니다.  
<img src="https://github.com/user-attachments/assets/3c627277-834b-4a4f-9cb3-577fb72664c5" width="60%" height="60%"/>

#### 2. 진동 제어 구현
노면 상태 별로 진동의 주파수와 강도를 다르게 반영한 진동 제어 코드를 보드에 저장하였습니다.
<img src="https://github.com/user-attachments/assets/ee35f062-874a-4b1d-8874-4f1c7bf727c1" width="60%" height="60%"/>

#### 3. 시뮬레이션과 햅틱 의자의 연동
<img src="https://github.com/user-attachments/assets/7a3baa5b-1f4f-471e-a192-e7d6c5d51cba" width="60%" height="60%"/>

## 기대 효과
- 감각 및 인지 기능을 동시적으로 향상할 수 있습니다.
- 반복 훈련이 지루하지 않고, 지속적인 재활 훈련을 유도할 수 있습니다.
- 병원 및 요양 시설 등에서 시뮬레이션 훈련 도구로 활용할 수 있습니다.
